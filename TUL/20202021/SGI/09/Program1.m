clc;clear all;close all;
fileID = fopen('FileList.txt','r');
textdata = textscan(fileID,'%s');  
fclose(fileID); 
fileNames = string(textdata{:});
numFiles = size (fileNames, 1);
count = 0;
for i = 1:numFiles
    x = double(imread(fileNames(i)));
    y = toGrayScale(x);
    y = evenSpread(y);
    y = filter2d(y,[1 1 1; 1 1 1; 1 1 1]);
    y = filter2d(y,[-1 -1 -1; -1 20 -1; -1 -1 -1]);
    y = boostContrast(y,evenSpread(toGrayScale(x)));
    y = centred(y,evenSpread(toGrayScale(x)));
    count = count + 1;
    showImage(x,y,count);
    if(count==10)
        count = 0;
        pause
    end
end

function y = toGrayScale(x)
    if(size(x,3)>1)
        y = (x(:,:,1)+x(:,:,2)+x(:,:,3))./3;
    else
        y = x;
    end
end

function y = centred(x,org)
    y=x;
    z=zeros(32,32);
    bl = min(y(:)) + 90;
    for i=1:size(x,1)
        for j=1:size(x,2)
            if(y(i,j) < bl)
                z(i,j) = org(i,j);
            end
        end
    end
    z = 255-z;
    c = 1 : size(z, 2); 
    r = 1 : size(z, 1); 
    [X, Y] = meshgrid(c, r);
    meanY = mean(z(:));
    centerOfMassX = mean(z(:) .* X(:)) / meanY;
    centerOfMassY = mean(z(:) .* Y(:)) / meanY;
    
    y = circshift(y,ceil(16-centerOfMassX),2);
    y = circshift(y,ceil(16-centerOfMassY),1);
end

function y = boostContrast(x,org)
    y=x;
    bl = min(y(:)) + 80;
    for i=1:size(x,1)
        for j=1:size(x,2)
            if(y(i,j) < bl)
                y(i,j) = org(i,j);
            end
        end
    end
end

function y = evenSpread(x)
    y =(x./max(x(:))).*255;
end

function dp = filter2d(x,f)
    dp = x;
    for n=1:size(x,1)
        for m=1:size(x,2)
            part = x(max(1,n-1):min(n+1,end),max(1,m-1):min(end,m+1));
            partf = f(1:size(part,1),1:size(part,2));
            part = part.*partf;
            dp(n,m) = sum(part(:))./sum(partf(:));
        end
    end
end

function showImage(x,y,i)
    subplot(10,2,i*2-1);
    image(uint8(x));
    subplot(10,2,i*2);
    image(uint8(y));
    colormap(gcf, gray(256));
end
