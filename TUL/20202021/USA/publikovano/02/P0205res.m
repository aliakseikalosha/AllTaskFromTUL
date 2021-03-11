clear all
clc

% vzorec pro výpoèet
%(M nad m)*(N-M nad n-m)/(N nad n)

%celkem èísel v osudí       N=80
%losováno èísel             n=20
%vybíráno                   M=10
%èísel uhádnuto             m

warning off
format long

for i=0:10
    d(i+1)=nchoosek(10,i)*nchoosek(80-10,20-i)/nchoosek(80,20);
end
d
%kontrola správnosti, souèet pravdìpodobností, že je 1
sum(d)

warning on
format short

