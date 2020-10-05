A = [2,-3,8;4,6,-7;-5,4,-9];
detA=0;
i=1;
for k=1:size(A, 2)
    AT=A;
    AT(:,k)=[];
    AT(i,:)=[];
    detA = detA +((-1)^(i+k))*A(i,k)*det(AT);
end
disp(detA)