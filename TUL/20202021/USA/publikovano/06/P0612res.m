%v�po�et pomoc� binomick�ho rozd�len�
n=600;
p=1/6;
pravd=1-binocdf(104.5,n,p)
%v�po�et pomoc� Poissonova rozd�len�
lambda=n*p;
pravd=1-poisscdf(104.5,lambda)
%v�po�et pomoc� CLV
mu=lambda;
sigma=sqrt(lambda);
pravd=1-normcdf(104.5,mu,sigma)