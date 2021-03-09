savefile='P0533.mat';
x=normrnd(10,40,1,100);
x=[x,25,24,26,43,65,98,121,187,195,213,321];
save(savefile,'x');