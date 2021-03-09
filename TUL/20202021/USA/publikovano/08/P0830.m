savefile='P0830.mat';
x1=exprnd(16,1,100);
x2=normrnd(16.4,4.3,1,100);
x3=normrnd(17.2,3.9,1,100);
x4=normrnd(15.7,5.5,1,100);
x5=normrnd(15.7,6.1,1,100);

x=[x1,x2,x3,x4,x5]';

save(savefile,'x');