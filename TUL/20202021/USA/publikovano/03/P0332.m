clear all
clc

savefile='P0332.mat';
%normální rozdìleníù
x=normrnd(24,8,6000,1);
save(savefile,'x');