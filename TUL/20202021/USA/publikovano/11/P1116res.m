for i=1:100
    x(i)=i;
end

for i=1:50
    y(2*i-1)=(i-1)-(i-1).*(i-1);
    y(2*i)=(i-1)+(i-1).*(i-1);
end

plot(x,y)
LM=LinearModel.fit(x,y,'quadratic')

modelfun=@(b,x)(b(1)+b(2).*x+b(3).*x.^2);
beta0=[0,0,1]
NLM=NonLinearModel.fit(x,y,modelfun,beta0)