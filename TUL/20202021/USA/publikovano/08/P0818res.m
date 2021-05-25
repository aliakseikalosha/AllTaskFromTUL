clear all
clc

varA=1.35;
varB=0.32;
podilAB=2;

nA=20;
nB=10;

%výpočet podle vzorce
T=(varA/2)/(varB/1)

test_min=finv(0.025,nA-1,nB-1)
test_max=finv(0.975,nA-1,nB-1)

if (T<test_min || T>test_max)
    'H1'
else
    'H0'
end

pval=2*min(fcdf(T,nA-1,nB-1),1-fcdf(T,nA-1,nB-1))