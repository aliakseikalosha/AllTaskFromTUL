savefile='P0910.mat';

x=normrnd(20,10,100,1)
data=[43,52,61,78,85,81,74,-15,-36,-23]'
x=[x;data];
save(savefile,'x')