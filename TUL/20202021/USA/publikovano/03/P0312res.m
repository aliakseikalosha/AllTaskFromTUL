clear all
clc

%otevøe data uložená v souboru
data=importdata('P0312.mat');
x=data(:,1);
y=data(:,2);

subplot(2,2,1)
ecdf(x)
title('distribuèní funkce vektoru x');
xlabel('data');
ylabel('distribuèní funkce');

z=x+y;
subplot(2,2,2)
ecdf(z)
title('distribuèní funkce vektoru x+y');
xlabel('data');
ylabel('distribuèní funkce');

%.* pøedstavuje skalární násobení. Jinak je maticové
z=x.*y;
subplot(2,2,3)
ecdf(z)
title('distribuèní funkce vektoru x*y');
xlabel('data');
ylabel('distribuèní funkce');

z=x./y;
subplot(2,2,4)
ecdf(log10(z))
title('distribuèní funkce vektoru x/y, vodorovná osa v log souøadnicích');
xlabel('logaritmus z dat');
ylabel('distribuèní funkce');
