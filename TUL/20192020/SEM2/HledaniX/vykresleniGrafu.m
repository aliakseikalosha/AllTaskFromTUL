v=1:7;
poc=zeros(7,1);
poc1=zeros(7,1);
poc2=zeros(7,1);
for i=1:length(v)
    poc(i)=JacobMet(velkRidkMat(i), eps);
    poc1(i)=GausSeid(velkRidkMat(i), eps);
    poc2(i)=sor(velkRidkMat(i), eps);
end
plot(v, poc)
hold on
plot(v, poc1, 'g')
plot(v, poc2, 'r')