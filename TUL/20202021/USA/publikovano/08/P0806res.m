clear all
clc

str_hod=27400;
sm_odch=5400;
n=50;

test=(str_hod-30000)*sqrt(n)/sm_odch

%porovnání výsledkù
test_dolni=tinv(0.025,n-1)
test_horni=tinv(0.975,n-1)
 
if (test<test_horni)&(test>test_dolni)
    'Hypotézu H0 na hladinì významnosti 5 % nezamítáme'
else
    'Hypotézu H0 na hladinì významnosti 5 % zamítáme'
end

p_value=tcdf(test,n-1);
p_value=2*min(p_value,1-p_value)