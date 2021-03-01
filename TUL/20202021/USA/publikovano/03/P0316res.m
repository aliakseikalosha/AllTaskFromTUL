%otev�e data ulo�en� v souboru
x=importdata('P0316.mat');
q5procent=quantile(x,0.05)
q50procent=quantile(x,0.50)
q95procent=quantile(x,0.95)
ecdf(x)

x=sort(x);

delka_vektoru=1/length(x);
y=0.5*delka_vektoru:delka_vektoru:(1-0.5*delka_vektoru);

plot(x,y)
title('Distribu�n� funkce nam��en�ch dat');
xlabel('Nam��en� data');
ylabel ('Distribu�n� funkce');
