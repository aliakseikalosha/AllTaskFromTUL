clc;clear all;close all;
A = [-1 4 -1;-2 -1 -11;2 10 2];
m = size(A,1);
Q=eye(m);
R=A;
n=0;
for i=1:m
n= n + m-i;
end
for j=1:n
    for i=m:(-1):j+1
        x=R(:,j);
        sqrtX = sqrt(x(i-1).^2 + x(i).^2);
        if sqrtX > 0
            cos=x(i-1)/sqrtX;
            sin=-x(i)/sqrtX;
            G=eye(m);
            G([i-1,i],[i-1,i])=[cos,sin;-sin,cos];
            R=G'*R;
            Q=Q*G;
        end
    end
end
A
Q
R