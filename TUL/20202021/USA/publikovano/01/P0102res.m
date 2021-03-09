clear all
clc

% a)
n=20;
vysledek=1;
for i=1:n
    vysledek=vysledek*i;
end
vysledek

% b)
vysledek2=factorial(n)

% c)
% faktoriál je souèin, po zlogaritmování obdržím souèet
format long %výpis je na 15 desetinných míst
ln_vysledek=0;
for i=1:1000000
    ln_vysledek=ln_vysledek+log10(i);
end
ln_vysledek
exponent=floor(ln_vysledek)
mantisa=10^(ln_vysledek-exponent)

