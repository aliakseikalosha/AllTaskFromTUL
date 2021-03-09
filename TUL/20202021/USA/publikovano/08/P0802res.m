prumer=3118;
n=25;
sd=357;

%pøípad ad a)
sigma=300;
%výpoèet se provádí ze základního vzorce T=s*s*(n-1)/(sigma*sigma)
test=sd*sd/(sigma*sigma)*(n-1)
atest=chi2inv(0.975,n-1)
if (test<atest)
    'H0'
else
    'H1'
end

%porovnání výsledkù ad b)
sigma=400;
test=sd*sd/(sigma*sigma)*(n-1)
btest=chi2inv(0.025,n-1)
if (test>btest)
    'H0'
else
    'H1'
end
