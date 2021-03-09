%v�po�et pomoc� binomick�ho rozd�len�
n=150;
p=16/30;
pravd=binopdf(77,n,p)
%v�po�et pomoc� Poissonova rozd�len�
lambda=n*p;
pravd=poisspdf(77,lambda)
%v�po�et pomoc� CLV
mu=lambda;
sigma=sqrt(lambda);
% nelze p�es normpdf, proto�e pravd�podobnost, �e bude p�esn� 77 je nulov�.
% p�echod z diskr�tn�ho na spojit� rozd�len�
pravd=normcdf(77.5,mu,sigma)-normcdf(76.5,mu,sigma)