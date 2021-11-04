// Callback function called, when file is "opened"
function handleFileSelect(item) {
	var files = item.files;

	console.log(files);

	for (var i = 0; i < files.length; i++) {
		console.log(files[i], files[i].name, files[i].size, files[i].type);

		// Only process image files.
		if (!files[i].type.match('image.*')) {
			continue;
		}

		var reader = new FileReader();

		// Closure for loading image to memory
		reader.onload = (function(file) {
			return function(evt) {

				var srcImg = new Image();
				srcImg.src = evt.target.result;

				srcImg.onload = function() {
					var srcCanvas = document.getElementById("src");
					var srcContext = srcCanvas.getContext("2d");
					var histCanvas = document.getElementById("histogram");
					var histContext = histCanvas.getContext("2d");
					
					// Change size of canvas
					srcCanvas.height = histCanvas.height = srcImg.height;
					srcCanvas.width = histCanvas.width = srcImg.width;

					srcContext.drawImage(srcImg, 0, 0);

					var canvasHeight = srcCanvas.height;
					var canvasWidth = srcCanvas.width;
					var srcImageData = srcContext.getImageData(0, 0, canvasWidth, canvasHeight);

					var histHeight = histCanvas.height;
					var histWidth = histCanvas.width;
					var histImageData = histContext.getImageData(0, 0, histWidth, histHeight);

					convertImageData(srcImageData, histImageData);

					histContext.putImageData(histImageData, 0, 0);
				}
			}
		})(files[i]);

		reader.readAsDataURL(files[i]);

		break;
	};
};


// Function for converting raw data of image
function convertImageData(srcImageData, histImageData) {
	var srcData = srcImageData.data;
	var histData = histImageData.data;
	var hist = new Array(256).fill(0);
	// Go through the image using x,y coordinates
	var red, green, blue, gray;
	for (var pixelIndex = 0; pixelIndex < srcData.length; pixelIndex += 4) {
		red   = srcData[pixelIndex + 0];
		green = srcData[pixelIndex + 1];
		blue  = srcData[pixelIndex + 2];
		alpha = srcData[pixelIndex + 3];

		// Do magic at this place :-)
		var color = parseInt(red * 0.32 +  green * 0.64 + blue * 0.02);
		hist[color]++;
	}

	getPixelIndex = (x,y) => ( (histImageData.width * (histImageData.width - y)) + x) * 4
	var max = Math.max(...hist);
	for (let x = 0; x < histImageData.width; x++) {
		for (let y = 0; y < histImageData.height; y++) {
			pixelIndex = getPixelIndex(x,y);
			color = 255;
			hvalue = hist[parseInt(x/2)]
			if(y < histImageData.height*(hvalue/max)){
				color = 0;
			}
			histData[pixelIndex + 0] = color;
			histData[pixelIndex + 1] = color;
			histData[pixelIndex + 2] = color;
			histData[pixelIndex + 3] = 255;
		}
	}
};

