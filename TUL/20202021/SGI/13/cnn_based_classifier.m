clear all;
close all;
clc

% N...poèet testovacích obrázkù
% C...poèet tøíd

% naètení dat
% X_test...testovací data, rozmìr: 1000x1x32x32 = NxHx32x32
    %   N=1000...poèet testovacích obrázkù    
    %   H1=1...hloubka obrázku = 1
    %   32x32...velikost obrázku
% y_test...indexy tøíd testovacích dat, rozmìr: 1000x1 = Nx1
% load spoken_tren.mat; % res 85.4%
% load spoken_test.mat;
load digits_tren.mat; %res 85%
load digits_test.mat;

% load digits_tren.mat; %res 75.55%
% load digits_test_nez.mat;
% load spoken_tren.mat; %res 86.833%
% load spoken_test_nez.mat;
% test_trida = test_nez_trida;
% test_data = test_nez_data;


y_test = test_trida;

X_test = data_preprocessing_cnn(test_data,tren_data);
N = size(X_test,1);

%MATLAB indexuje od èisla 1 =>
%Pokud spoèítáme skóre pro èíslovky od 0 do 9, budou tato skóre v poli, kde
%skóre pro èíslovku 0 bude na pozici 1 a skóre pro èíslovku 9 na pozici 10.
%Pokud pak bude mít napø. èíslovka 0 maximální skóre, bude index nejlepší tøídy 1 nikoli 0.
%V referencich má ale èíslovka 0 index tøídy 0 a èíslovka 9 index tøídy 9 =>
%Pokud k tìmto referenèním indexùm pøièteme jedna, budou indexy
%odpovídat MATLABU a usnadníme si vyhodnocování úspìšnosti rozpoznávání!
y_test = y_test + ones(1,N);

% naètení parametrù klasifikátoru
% load Spoken_CNN_4Layer_8F_8F_32H_10.mat%cnn_based_classifier_params.mat;
load Image_CNN_4Layer_8F_8F_32H_10.mat%cnn_based_classifier_params.mat;

tic
accuracy = 0;
%Vnìjší cyklus pro testovací data
%Po odladìní na prvním obrázku upravte na for n=1:N
for n=1:N
		
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    %I. Výpoèet první konvoluèní vrstvy%
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    % PARAMETRY
    % ___________________________________________________
    % CW1...matice vah první konvoluèní vrstvy
    % rozmìr: F1xH1x3x3 = 8x1x3x3
    %   F1 = 8...poèet filtrù konvoluèní vrstvy
        F1 = size(CW1,1);
    %   H1 = 1...hloubka vstupu konv.vrstvy = hloubka vstupnich obrázkù
    %   3x3...velikost konvoluèního filtru    
    % Cb1...matice posunù (biasù) první konvoluèní vrstvy
    % rozmìr: F1x1 = 8x1
    
    % VSTUP
    % ___________________________________________________
    % Jeden obrázek o barevné hloubce 1 v matici X_test
    %   rozmìr: H1x32x32 = 1x32x32
    
    % VÝSTUP
    % ___________________________________________________
    % 8 vrstev v matici convout1
    %   rozmìry: F1x32x32 = 8x32x32
    %       proè 32?
    %       32 = 32 (velikost obr.) + 2 (rozšíøení kvùli paddingu) - 3 (velikost filtru) + 1 
                   
    %Funkce squeeze odstraní první dimenzi matice a vytvoøí matici 32x32       
    X_pad = squeeze(X_test(n,:,:));
    
    % Cyklus pro jednotlivé filtry této konvoluèní vrstvy:
    for f=1:F1
        %Výpoèet konvoluce pro daný filtr
        convout1(f,:,:) = filter2(squeeze(CW1(f,:,:,:)), X_pad);
        
        %Pøiètení biasu:
        convout1(f,:,:) = convout1(f,:,:) + Cb1(f);
    end    
    %Aplikace aktivaèní funkce ReLU pro všechny konvoluèní filtry:
    convout1(convout1 < 0) = 0;
    
    % Kontrola: convout1(1,1,1)=0.0588
    % Kontrola: convout1(2,1,1)=0.0069
           
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    %II. Výpoèet druhé konvoluèní vrstvy%
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    
    % PARAMETRY
    % ___________________________________________________
    % CW2...matice vah pro druhou konvoluèní vrstvu
    % rozmìry: F2xH2x3x3 = 8x8x3x3
    %   F2 = 8...poèet filtrù druhé konvoluèní vrstvy
        F2 = size(CW2,1);
    %   H2 = 8...hloubka vstupu druhé konvoluèní vrstvy = poèet filtrù pøedchozí
    %   konvoluèní vrstvy => H2 = F1 !!!!!!!!!    
        H2 = size(CW2,2);    
    %   3x3...velikost konvoluèního filtru    
    % Cb2...matice posunù (biasù) druhé konvoluèní vrstvy
    % rozmìry: F2x1 = 8x1
    
    % VSTUP
    % ___________________________________________________
    % výstup z pøedchozí konvoluèní vrstvy o hlubce H1
    %   rozmìry: H2x32x32 = 8x32x32    
    
    % VÝSTUP
    % ___________________________________________________
    % 8 vrstev v matici convout2
    %   rozmìry: F2x32x32 = 8x32x32
    %       proè 32?
    %       32 = 32 (šíøka respektive výška vstupu) + 2 (rozšíøení kvùli paddingu) - 3 (velikost filtru) + 1         
    
    %Inicializace výstupu:
    convout2x = size(convout1,2)+2-size(CW2,3)+1;
    convout2y = size(convout1,3)+2-size(CW2,4)+1;
    convout2 = zeros(F2,convout2x,convout2y);
        
    %Cyklus pro jednotlivé filtry této konvoluèní vrstvy:
    for f=1:F2        
        temp = zeros(convout2x,convout2y);
        %Cyklus navíc pro jednotlivé vrstvy vstupu !!       
        for h=1:H2
            filtr = squeeze(CW2(f,:,:,:));
            X_pad = squeeze(convout1(h,:,:));
            temp(:,:) = temp(:,:) + filter2(squeeze(filtr(h,:,:)), X_pad);
        end
        convout2(f,:,:) = temp;
        %Pøiètení biasu:
        convout2(f,:,:) = convout2(f,:,:) + Cb2(f);
    end    
    %Aplikace aktivaèní funkce ReLU pro všechny výstupní konvoluèní filtry:
    convout2(convout2 < 0) = 0;
    a = 5;
    %Kontrola: convout2(1,1,1) = 0
    %Kontrola: convout2(3,2,1) = 0.0014
    %Kontrola: convout2(3,18,17) = 0
    
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    %III. Výpoèet max-pool vrstvy%
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    % VSTUP
    % ___________________________________________________
    % výstup z pøedchozí konvoluèní vrstvy o dané hlubce H2
    %   rozmìry: H2x32x32 = 8x32x32    
    
    % VÝSTUP
    % ___________________________________________________
    % 8 vrstev v matici maxpool
    %   rozmìry: F2x16x16 = 8x16x16
    %       proè 16?
    %       max pool filtr vybírá maximum z okenka 2x2
    %       výstupní velikost je poloèní oproti vstupní
    
    %Inicializace výstupu této vrstvy:
    maxpoolout = zeros(F2,convout2x/2,convout2y/2);
    
    %Cyklus pro jednotlivé filtry této vrstvy:
    for f=1:F2
        for x=1:convout2x/2
            for y=1:convout2y/2
                maxpoolout(f,x,y)=max(convout2(f,(x-1)*2+1:(x-1)*2+2,(y-1)*2+1:(y-1)*2+2),[],'all');
            end
        end        
    end
    % Kontrola: maxpoolout(2,3,5) = 0.1043
    
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    %IV. Výpoèet skryté vrstvy MLP  %
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    
    % PARAMETRY
    % ___________________________________________________
    % W1...matice vah této skryté vrstvy
    % b1...vektor biasù
    % rozmìr: DxNH = 2048x32
    %   D = 2048...8*16*16 = poèet hodnot na výstupu pøechozí max-pool
    %   vrstvy
    %   NH = 32...zvolený poèet neuronù skryté vrstvy MLP sítì                
    % VSTUP
    % ___________________________________________________
    % výstup z pøedchozí max-pool vrstvy
    %   rozmìry: D = 8*16*16    
    
    % VÝSTUP
    % ___________________________________________________
    % 32 hodnot
    
    %Pøevedeme matici 8x16x16 po øádcích na vektor 1x2048, maxpoolout(:) nelze použít, pøevoded je takto provádìn po sloupcích !!
    %Po øádcích pøevádíme proto, protože stejnì pøevádìl i trénovací software !!    
    tmp = permute(maxpoolout,[3 2 1]);
    tmp = reshape(tmp, [1 length(W1)]);
    
    %Násobení vahami a pøiètení biasu
    mlpout1 = tmp*W1 + b1;
    
    %ReLU:
    mlpout1(mlpout1<0)=0;
    
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    %V. Výpoèet výstupní vrstvy  %
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    
    % PARAMETRY
    % ___________________________________________________
    % W2...matice vah výstupní vrstvy
    % b2...vektor biasù výstupní vrstvy
    % rozmìr: NHxC = 32x10
    %   NH = 32...zvolený poèet neuronù skryté vrstvy MLP sítì        
    %   C = 10...poèet tøíd
    % VSTUP
    % ___________________________________________________
    % výstup z pøedchozí skryté vrstvy
    %   rozmìr: NH = 32    
    % VÝSTUP
    % ___________________________________________________
    %   rozmìr: C = 10 ... poèet tøíd
    
    %Násobení vahami a pøiètení biasu
    mlpout2 = mlpout1*W2 + b2;
    
    %Pro výbìr maxima zde již není potøeba aplikovat ReLU!   
    %Pokud ji aplikujeme, doje k oøíznutí všech hodnot < 0 na 0 a tím
    %pádem dostaneme pro nìkterý obrázek špatný výsledek klasifikace
    
    %%%%%%%%%%%%%%%%%%
    %V. KLASIFIKACE  %
    %%%%%%%%%%%%%%%%%%
    [maxvalue, predict] = max(mlpout2);        
    if (predict == y_test(n))
        accuracy = accuracy+1;
    end            
    if mod(n,10) == 0
        fprintf('n: %i, pøesnost je: %f\n', n,100*accuracy/n);
    end
    
        
end   
fprintf('Pøesnost je: %f\n', 100*accuracy/N);
toc 
