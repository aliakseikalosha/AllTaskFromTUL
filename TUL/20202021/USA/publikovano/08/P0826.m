savefile='P0826.mat';
x1=normrnd(16,4,1,100);
x2=normrnd(16.1,4.2,1,100);
x3=normrnd(16.2,3.8,1,100);
x4=normrnd(16.15,4,1,100);
x5=normrnd(16.3,4.1,1,100);

x=[x1,x2,x3,x4,x5]';

save(savefile,'x');