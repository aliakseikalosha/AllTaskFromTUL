clc;
clear all;
close all;
A = [1,1,0;1,1,1;0,1,1];
y = [1,1,1];
vlCislo=0;
eps = 10^-10;
k=0;
while abs(max(eig(A))- vlCislo)>eps 
    k=k+1;
    y0=y;
    for i=1:length(y)
        y(i)=sum(y0.*A(i,:));
    end
    vlCislo = sum(y./y0)/length(y);
    disp(k +" "+vlCislo);
end
