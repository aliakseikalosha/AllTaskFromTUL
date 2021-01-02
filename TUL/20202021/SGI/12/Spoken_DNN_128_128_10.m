clear all;
close all;
%clc
loadingSI = 1;
if loadingSI==1
    disp('SI')
    load spoken_test_nez;
    test_data = test_nez_data;
    test_trida = test_nez_trida;
else
    disp('SD')
    load spoken_test;
end
load spoken_test_nez;
% N...počet testovacích obrázků = 1000
% y_test...indexy tříd testovacích dat, rozměr: 1000x1 = Nx1
y_test = test_trida;
%MATLAB indexuje od čisla 1 =>
%Pokud spočítáme skóre pro číslovky od 0 do 9, budou tato skóre v poli, kde
%skóre pro číslovku 0 bude na pozici 1 a skóre pro číslovku 9 na pozici 10.
%Pokud pak bude mít např. číslovka 0 maximální skóre, bude index nejlepší třídy 1 nikoli 0.
%V referencich má ale číslovka 0 index třídy 0 a číslovka 9 index třídy 9 =>
%Pokud k těmto referenčním indexům přičteme číslo jedna, budou indexy
%sedět s MATLABEM a usnadníme si vyhodnocování úspěšnosti rozpoznávání!
N = size(y_test,2);
y_test = y_test + ones(1,N);

load spoken_tren;
% Testovací data je nutné nejprve předzpracovat:
    % Je nutné je normalizovat = odečíst střední hodnotu trénovacích dat
        % Případně je možné je i vycentrovat
    % Dále jsou v rámci předzpracování testovací data převedena z matice
    % Nx32x32 do matice Nx(32*32+1)
%X_test = data_preprocessing(test_data,tren_data);
X_test = data_preprocessing_fast(test_data,tren_data);

% načtení matic vah
%load Image_DNN_128_128_10.mat;
load Spoken_DNN_128_128_10.mat;

% W1 je matice vah první vrstvy, která má 128 neuronů
    %rozměry jsou 1025x128
% W2 je matice vah druhé vrstvy, která má také 128 neuronů
    %rozměry jsou 129x128
% W3 je matice vah třetí vrstvy, která má 10 neuronů
    %rozměry jsou 129x10    
    
%Nyní jsou k dispozici připravená testovací data v matici X_test a
%parametry klasifikátoru v maticích W1, W2 a W3

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%% KLASIFIKACE %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Výpočet bez použití cyklů !!!!%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

tic
N = size(X_test,1);
% ========================== VÁŠ KÓD ZDE ======================
%PRVNÍ VRSTVA:
%Nejprve skóre:
scores1 =X_test*W1;
%Následuje aplikace aktivační funkce ReLU (namísto sigmoidy)
%Tu lze zapsat bez použití cyklu FOR pomocí funkce FIND a toho, že MATLAB
%umožňuje "logické" indexování viz např.: https://www.mathworks.com/help/matlab/math/matrix-indexing.html?refresh=true
scores1(scores1<0) = 0;
%Rozšíření vypočtené matice o vektor jedniček:
scores1 = [scores1 ones(size(scores1,1),1)];
% Kontrola: scores1(666,111) = 7.2073

%DRUHÁ VRSTVA:
%Nejprve skóre:
scores2 = scores1*W2;
%Aplikace aktivační funkce ReLU (namísto sigmoidy)
scores2(scores2<0) = 0;
%Rozšíření vypočtené matice o vektor jedniček:
scores2 = [scores2 ones(size(scores2,1),1)];
% Kontrola: scores2(666,111) = 6.5808

%TŘETÍ VRSTVA:
%Skóre:
scores3 = scores2*W3;
%Neaplikujte již funkci ReLU, pro hledání maxima není potřeba, po její
%aplikaci naopak dostanete skóre jen 74.6 % procent, protože pokud je více
%maxim < 0, všechna tato maxima se stanou nulovými hodnotami a nebude
%vybráno skutečné správné maximum

%Funkce max pak zjistí pozice maximální hodnoty v každém řádku předchozí
%matice, výsledkem je tedy vektor nejlepších skóre o rozměrech 1*N
[vektor predicts] =  max(scores3,[],2);
%Výpočet přesnosti lze pak zapsat na jeden řádek elegantně jako:
accuracy = mean(double(predicts == y_test')) * 100;
% =============================================================
% Kontrola: accuracy = 75.9 %
fprintf('Přesnost maticově: %f\n', accuracy);
toc