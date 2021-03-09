%otevøe data uložená v souboru
data=importdata('P0312.mat');
x=data(:,1);
y=data(:,2);

a=0.00005:0.0001:0.99995;

xsetr=sort(x);
subplot(2,2,1)
plot(xsetr,a)
title('distribuèní funkce vektoru x');
xlabel('data');
ylabel('distribuèní funkce');

z=x+y;
zsetr=sort(z);
subplot(2,2,2)
plot(zsetr,a)
title('distribuèní funkce vektoru x+y');
xlabel('data');
ylabel('distribuèní funkce');

z=x.*y;
zsetr=sort(z);
subplot(2,2,3)
plot(zsetr,a)
title('distribuèní funkce vektoru x*y');
xlabel('data');
ylabel('distribuèní funkce');

z=x./y;
zsetr=sort(z);
zsetr=log10(zsetr);
subplot(2,2,4)
plot(zsetr,a)
title('distribuèní funkce vektoru x/y, vodorovná osa v log souøadnicích');
xlabel('data');
ylabel('distribuèní funkce');
