prumer=3118;
n=25;
sd=357;

%p��pad ad a)
sigma=300;
%v�po�et se prov�d� ze z�kladn�ho vzorce T=s*s*(n-1)/(sigma*sigma)
test=sd*sd/(sigma*sigma)*(n-1)
atest=chi2inv(0.975,n-1)
if (test<atest)
    'H0'
else
    'H1'
end

%porovn�n� v�sledk� ad b)
sigma=400;
test=sd*sd/(sigma*sigma)*(n-1)
btest=chi2inv(0.025,n-1)
if (test>btest)
    'H0'
else
    'H1'
end
