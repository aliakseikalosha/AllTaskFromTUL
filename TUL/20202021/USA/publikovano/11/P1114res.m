vstup=importdata('P1113.xlsx');
x=vstup.data.List1(:,1);
y=vstup.data.List1(:,2);

modelfun=@(b,x)(b(1).*sin(b(2).*x+b(3)));
beta0=[1,1,0]
NLM=fitnlm(x,y,modelfun,beta0)