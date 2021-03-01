vstup=importdata('P1118.xlsx');
x=vstup.data.List1(:,1);
y=vstup.data.List1(:,2);


modelfun=@(b,x)(exp(b(1)+b(2).*x));
beta0=[1,10]
NLM=NonLinearModel.fit(x,y,modelfun,beta0)

y=log(y);
LM=LinearModel.fit(x,y)