clear all
clc

savefile='P0801.mat';
for i=1:100
    x(i)=normrnd(97,1.52);
end

save(savefile,'x');