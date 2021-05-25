clear all
clc

n=1000;
p=82/1000;

pi=0.15;

T=(p-pi)*sqrt(n)/sqrt(pi*(1-pi))

if (T<norminv(0.025,0,1) || T>norminv(0.975,0,1))
    'Hypotézu H0 na hladině významnosti 5 % zamítáme'
else
    'Hypotézu H0 na hladině významnosti 5 % přijímáme'
end