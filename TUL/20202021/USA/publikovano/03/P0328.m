clear all
clc

savefile='P0328.mat';
%normální rozdìlení støední hodnota 1000, rozptyl 160000
x=normrnd(1000,400,100,1);
save(savefile,'x');