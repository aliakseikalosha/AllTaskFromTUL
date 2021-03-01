%otev�e data ulo�en� v souboru
data=importdata('P0312.mat');
x=data(:,1);
y=data(:,2);

a=0.00005:0.0001:0.99995;

xsetr=sort(x);
subplot(2,2,1)
plot(xsetr,a)
title('distribu�n� funkce vektoru x');
xlabel('data');
ylabel('distribu�n� funkce');

z=x+y;
zsetr=sort(z);
subplot(2,2,2)
plot(zsetr,a)
title('distribu�n� funkce vektoru x+y');
xlabel('data');
ylabel('distribu�n� funkce');

z=x.*y;
zsetr=sort(z);
subplot(2,2,3)
plot(zsetr,a)
title('distribu�n� funkce vektoru x*y');
xlabel('data');
ylabel('distribu�n� funkce');

z=x./y;
zsetr=sort(z);
zsetr=log10(zsetr);
subplot(2,2,4)
plot(zsetr,a)
title('distribu�n� funkce vektoru x/y, vodorovn� osa v log sou�adnic�ch');
xlabel('data');
ylabel('distribu�n� funkce');
