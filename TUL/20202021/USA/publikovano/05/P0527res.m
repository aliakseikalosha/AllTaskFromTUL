%kvantil 2,5% a 97,5% normovaného normálního rozdìlení
%Interval uprostøed má 95 % a zjistíme sigmu
%použijeme inverzní transformaci z normovaného normálního na normální
%rozdìlení

zmin=norminv(0.025,0,1)
zmax=norminv(0.975,0,1)

%z=(x-mu)/sigma, z toho z*sigma=x (mu je nula)
%+20 metrù odpovídá 1.96 sigma
%-20 metrù odpovídá -1.96 sigma

sigma=20/zmax




