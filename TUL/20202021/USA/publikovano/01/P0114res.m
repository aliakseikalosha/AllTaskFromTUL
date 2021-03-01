clear all
clc
%n=7

% a) kombinace s opakováním, protože nezáleží na poøadí a mohou se pohlednice
%opakovat
%k=koupíme 10 pohlednic

vysledek_a=nchoosek(7+10-1,10)

%pøíklad b
%kombinace s opakováním
%k=5
vysledek_b=nchoosek(7+5-1,5)

%pøíklad c
%kombinace bez opakování, musí být rùzné pohlednice
%n=7
%k=5
vysledek_c=nchoosek(7, 5)
