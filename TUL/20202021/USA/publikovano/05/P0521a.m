x=[30,70,120,180,230,290,320,360,430,510];
E=expfit(x)
W=wblfit(x)
probplot('exponential',x)
%wblplot(x)
