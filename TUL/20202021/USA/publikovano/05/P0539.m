savefile='S0114.mat';

a=normrnd(1000,100,50,1);
b=normrnd(1050,120,50,1);
c=normrnd(1120,160,50,1);
d=normrnd(1190,250,50,1);
x=[a,b,c,d];


save(savefile,'x');
