%pravd�podobnost, �e padne orel je 0.5
%zjist�me hodnotu distribu�n� funkce v bod� 520 a ode�teme pro 480

%binomick� rozd�len�
binocdf(520,1000,0.5);
binocdf(480,1000,0.5);
vysledek_a=binocdf(520,1000,0.5)-binocdf(480,1000,0.5)

%poissonovo rozd�len�
%lambda=0.5*1000 pokus�
poisscdf(520,500);
poisscdf(480,500);
vysledek_b=poisscdf(520,500)-poisscdf(480,500)