%výpoèet pomocí binomického rozdìlení
n=600;
p=1/6;
pravd=1-binocdf(104.5,n,p)
%výpoèet pomocí Poissonova rozdìlení
lambda=n*p;
pravd=1-poisscdf(104.5,lambda)
%výpoèet pomocí CLV
mu=lambda;
sigma=sqrt(lambda);
pravd=1-normcdf(104.5,mu,sigma)