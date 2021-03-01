%výpoèet pomocí binomického rozdìlení
n=150;
p=16/30;
pravd=binopdf(77,n,p)
%výpoèet pomocí Poissonova rozdìlení
lambda=n*p;
pravd=poisspdf(77,lambda)
%výpoèet pomocí CLV
mu=lambda;
sigma=sqrt(lambda);
% nelze pøes normpdf, protože pravdìpodobnost, že bude pøesnì 77 je nulová.
% pøechod z diskrétního na spojité rozdìlení
pravd=normcdf(77.5,mu,sigma)-normcdf(76.5,mu,sigma)