n=1000;
p=82/1000;

pi=0.15;

T=(p-pi)*sqrt(n)/sqrt(pi*(1-pi))

if (T<norminv(0.025,0,1) || T>norminv(0.975,0,1))
    'H1'
else
    'H0'
end