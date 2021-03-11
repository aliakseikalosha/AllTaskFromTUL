clear all
clc

% pravdepodobnost 4 zasahu
% nchoosek(4,4)=1, a musí se vždy trefit 0.1*0.1*0.1*0.1
p4=nchoosek(4,4)*0.1^4

% pravdìpodobnost 3 zásahù
% nchoosek (4,3) poèet kombinací kdy se trefí 3x ze 4 pokusù
% 0.1^3 musí se trefit 3x
% 0.9 pravdìpodobnost netrefení se a netrefí se jednou
p3=nchoosek(4,3)*0.1^3*0.9;

%pravdìpodobnost 2 zasahù
p2=nchoosek(4,2)*0.1^2*0.9^2;

%pravdìpodobnost 1 zásahu
p1=nchoosek(4,1)*0.1*0.9^3;

%pravdìpodobnost 0 zásahù
p0=nchoosek(4,0)*0.9^4;

%kontrola souètu pravdìpodobností
p=p0+p1+p2+p3+p4

vysledek=p4*0.9+p3*0.9+p2*0.4+p1*0.1