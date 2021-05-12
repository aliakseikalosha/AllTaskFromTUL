clear all
clc
%data
nMesto=1240;
nVesnice=741;

anoMesto=325;
anoVesnice=287;

%poměry
pM=anoMesto/nMesto
pV=anoVesnice/nVesnice

T=(pM-pV)/sqrt((pM*(1-pM)/nMesto)+(pV*(1-pV)/nVesnice))

if (T<norminv(0.025,0,1)||T>norminv(0.975,0,1))
    'H1'
else
    'H0'
end

%výpočet pvalue
pom=normcdf(T,0,1);
pval=2*min(pom,1-pom)