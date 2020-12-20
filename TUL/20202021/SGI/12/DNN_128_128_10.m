clear all;
close all;
clc

load digits_test_nez;
% N...poèet testovacích obrázkù = 1000
% y_test...indexy tøíd testovacích dat, rozmìr: 1000x1 = Nx1
y_test = test_nez_trida;
%MATLAB indexuje od èisla 1 =>
%Pokud spoèítáme skóre pro èíslovky od 0 do 9, budou tato skóre v poli, kde
%skóre pro èíslovku 0 bude na pozici 1 a skóre pro èíslovku 9 na pozici 10.
%Pokud pak bude mít napø. èíslovka 0 maximální skóre, bude index nejlepší tøídy 1 nikoli 0.
%V referencich má ale èíslovka 0 index tøídy 0 a èíslovka 9 index tøídy 9 =>
%Pokud k tìmto referenèním indexùm pøièteme èíslo jedna, budou indexy
%sedìt s MATLABEM a usnadníme si vyhodnocování úspìšnosti rozpoznávání!
N = size(y_test,2);
y_test = y_test + ones(1,N);

load digits_tren;
% Testovací data je nutné nejprve pøedzpracovat:
    % Je nutné je normalizovat = odeèíst støední hodnotu trénovacích dat
        % Pøípadnì je možné je i vycentrovat
    % Dále jsou v rámci pøedzpracování testovací data pøevedena z matice
    % Nx32x32 do matice Nx(32*32+1)
%X_test = data_preprocessing(test_data,tren_data);
test_data = test_nez_data;
for i = 1:size(test_data,1)
    x = test_data(i,:,:);
    x = squeeze(x(1,:,:));
    test_data(i,:,:) = centerImage(x);
end
for i = 1:size(tren_data,1)
    x = tren_data(i,:,:);
    x = squeeze(x(1,:,:));
    tren_data(i,:,:) = centerImage(x);
end
X_test = data_preprocessing_fast(test_data,tren_data);

% naètení matic vah
%load Image_DNN_128_128_10.mat;
load Image_centered_DNN_128_128_10.mat;

% W1 je matice vah první vrstvy, která má 128 neuronù
    %rozmìry jsou 1025x128
% W2 je matice vah druhé vrstvy, která má také 128 neuronù
    %rozmìry jsou 129x128
% W3 je matice vah tøetí vrstvy, která má 10 neuronù
    %rozmìry jsou 129x10    
    
%Nyní jsou k dispozici pøipravená testovací data v matici X_test a
%parametry klasifikátoru v maticích W1, W2 a W3

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%% KLASIFIKACE %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Výpoèet bez použití cyklù !!!!%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

tic
N = size(X_test,1);
% ========================== VÁŠ KÓD ZDE ======================
%PRVNÍ VRSTVA:
%Nejprve skóre:
scores1 =X_test*W1;
%Následuje aplikace aktivaèní funkce ReLU (namísto sigmoidy)
%Tu lze zapsat bez použití cyklu FOR pomocí funkce FIND a toho, že MATLAB
%umožòuje "logické" indexování viz napø.: https://www.mathworks.com/help/matlab/math/matrix-indexing.html?refresh=true
scores1(scores1<0) = 0;
%Rozšíøení vypoètené matice o vektor jednièek:
scores1 = [scores1 ones(size(scores1,1),1)];
% Kontrola: scores1(666,111) = 7.2073

%DRUHÁ VRSTVA:
%Nejprve skóre:
scores2 = scores1*W2;
%Aplikace aktivaèní funkce ReLU (namísto sigmoidy)
scores2(scores2<0) = 0;
%Rozšíøení vypoètené matice o vektor jednièek:
scores2 = [scores2 ones(size(scores2,1),1)];
% Kontrola: scores2(666,111) = 6.5808

%TØETÍ VRSTVA:
%Skóre:
scores3 = scores2*W3;
%Neaplikujte již funkci ReLU, pro hledání maxima není potøeba, po její
%aplikaci naopak dostanete skóre jen 74.6 % procent, protože pokud je více
%maxim < 0, všechna tato maxima se stanou nulovými hodnotami a nebude
%vybráno skuteèné správné maximum

%Funkce max pak zjistí pozice maximální hodnoty v každém øádku pøedchozí
%matice, výsledkem je tedy vektor nejlepších skóre o rozmìrech 1*N
[vektor predicts] =  max(scores3,[],2);
%Výpoèet pøesnosti lze pak zapsat na jeden øádek elegantnì jako:
accuracy = mean(double(predicts == y_test')) * 100;
% =============================================================
% Kontrola: accuracy = 75.9 %
fprintf('Pøesnost maticovì: %f\n', accuracy);
toc



function y = centerImage(im)
    
    im_fil = filter(im, ([1 1 1;1 1 1;1 1 1]));    
    
    [N,edges] = histcounts(im_fil,255/15);
    [max_hist, max_hist_i] = max(N);
    k = 255 / edges(max_hist_i);
    y = zeros(size(im));
    for i = 1 : size(im_fil,1)
        for j = 1 : size(im_fil,2)
            y(i , j) = im_fil(i , j) * k;
        end
    end
    
    pic_center = round([size(y,1)/2, size(y,2)/2]);
    
    cent = getCentroid(y);
    
%     if size(cent) ~= [1 2]
%         disp('Chyba')
%     end
    
    y = circshift(y, [(pic_center(2) - cent(2)) (pic_center(1) - cent(1))]);
end

function cent = getCentroid(x)
    [N,edges] = histcounts(x,255/15);
    [max_hist, max_hist_i] = max(N);
%     tricet funguje asi dobre
    bloby = bwareafilt(x < edges(max_hist_i), [30, inf]);
    
    stats = regionprops(bloby, 'Centroid');
    cent = cat(1,stats.Centroid);
    
    cent = round(cent);
end

function y = filter(x,h)
    y = x;
    for m = 1:size(x,1)
        for n = 1:size(x,2)
            x_cube = x(max(1,m-1):min(m+1,end), max(1,n-1):min(end,n+1));
            h_cube = h(1:size(x_cube,1), 1:size(x_cube,2));
            x_cube = x_cube.*h_cube;
            y(m,n) = sum(x_cube(:))./sum(h_cube(:));
        end
    end
end
