n=3;
A=[1,1/2,1/3;1/2,1/3,1/4;1/3,1/4,1/5];
b=[1;3;5];

L1=chol(A, 'lower');
L = 0;
for j = 1:n
    if j == 1
        L(j,j) = sqrt(A(j,j));
        L(j+1:n,j) = A(j+1:n,j)/L(j,j);
    else
        v = L(j,1:j-1)';
        L(j,j) = sqrt(A(j,j)-L(j,1:j-1)*v);
        L(j+1:n,j) = (A(j+1:n,j)-L(j+1:n,1:j-1)*v)/L(j,j);
    end
end
L
L1
y=L\b; 
x=L'\y



