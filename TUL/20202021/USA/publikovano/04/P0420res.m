%pravdìpodobnost, že padne orel je 0.5
%zjistíme hodnotu distribuèní funkce v bodì 520 a odeèteme pro 480

%binomické rozdìlení
binocdf(520,1000,0.5);
binocdf(480,1000,0.5);
vysledek_a=binocdf(520,1000,0.5)-binocdf(480,1000,0.5)

%poissonovo rozdìlení
%lambda=0.5*1000 pokusù
poisscdf(520,500);
poisscdf(480,500);
vysledek_b=poisscdf(520,500)-poisscdf(480,500)