clear all
clc

%pravdìpodobnost, že padne orel je 0.5
%zjistíme hodnotu distribuèní funkce v bodì 520 a odeèteme pro 480

%binomické rozdìlení
vysledek_a=binocdf(520.5,1000,0.5)-binocdf(479.5,1000,0.5)

%poissonovo rozdìlení
%lambda=0.5*1000 pokusù
vysledek_b=poisscdf(520.5,500)-poisscdf(479.5,500)