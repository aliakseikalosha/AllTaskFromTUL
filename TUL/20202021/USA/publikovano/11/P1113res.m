vstup=importdata('P1113.xlsx');
x=vstup.data.List1(:,1);
y=vstup.data.List1(:,2);

modelfun=@(b,x)(b(1).*sin(b(2).*x+b(3)));
beta0=[5,1/3,1]
NLM=fitnlm(x,y,modelfun,beta0)