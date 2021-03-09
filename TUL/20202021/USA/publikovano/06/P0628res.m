%Fischerovo rozdìlení s 10 a 5 stupni volnosti
kvant5a=finv(0.05,10,5)
kvant95a=finv(0.95,10,5)

%Fischerovo rozdìlení s 5 a 10 stupni volnosti
kvant5b=finv(0.05,5,10)
kvant95b=finv(0.95,5,10)

%ovìøení správnosti výsledkù
vysledek=kvant5a-1/kvant95b
vysledek=kvant5b-1/kvant95a