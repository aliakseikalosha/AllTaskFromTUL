savefile='S0109.mat';
%normální rozdìlení støední hodnota 1000, rozptyl 16000
x=normrnd(1000,400,100,1);
save(savefile,'x');