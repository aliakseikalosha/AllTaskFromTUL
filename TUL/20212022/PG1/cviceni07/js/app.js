class Player {
    constructor(speed, size, obj = null) {
        this.speed = speed;
        this.size = size;
        this.direction = 0.0;
        this.obj = obj;
        this.score = 0;
    }

    connectObject(obj) {
        this.obj = obj;
    }

    startMoveUp() {
        this.direction = 1;
    }

    stopMoveUp() {
        if (this.direction > 0) {
            this.direction = 0;
        }
    }

    startMoveDown() {
        this.direction = -1;
    }

    stopMoveDown() {
        if (this.direction < 0) {
            this.direction = 0;
        }
    }

    update(deltaTime) {
        this.obj.position.x += this.direction * deltaTime * this.speed;
    }

    setPosition(x, y, z) {
        this.obj.position.set(x, y, z);
    }
}


window.onload = function () {
    const RELEASE = true;
    const MUSIC_VOLUME = 0.5;
    const SFX_VOLUME = 0.5;
    const BALL_INITIAL_SPEED = 5;
    const BALL_MAX_SPEED = 15;
    const BALL_RADIUS = 0.25;
    const PLAYER_SIZE = new THREE.Vector3(2.5, 1, 0.3);
    const AI = [null, null]
    let stats;

    let camera, scene, renderer, level, ball,
        arenaSize = new THREE.Vector3(20, 1, 40),
        playerLeft = new Player(15, PLAYER_SIZE),
        playerRight = new Player(15, PLAYER_SIZE),
        clock = new THREE.Clock(),
        bounceSound;

    const scoreLeft = document.getElementById("scoreLeft");
    const scoreRight = document.getElementById("scoreRight");

    const onePlayer = document.getElementById("onePlayer");
    const twoPlayer = document.getElementById("twoPlayer");
    const watchGame = document.getElementById("justWatching");
    const menu = document.getElementById("menu");

    const keyMapDown = {
        /*Camera rotation controle
        'w': () => camera.rotation.y += 0.1,
        's': () => camera.rotation.y -= 0.1,
        'a': () => camera.rotation.x += 0.1,
        'd': () => camera.rotation.x -= 0.1,
        'p': () => camera.rotation.z += 0.1,
        'l': () => camera.rotation.z -= 0.1,
         */
        'w': () => playerLeft.startMoveUp(),
        's': () => playerLeft.startMoveDown(),
        'ArrowUp': () => playerRight.startMoveUp(),
        'ArrowDown': () => playerRight.startMoveDown(),
    }
    const keyMapUp = {
        'w': () => playerLeft.stopMoveUp(),
        's': () => playerLeft.stopMoveDown(),
        'ArrowUp': () => playerRight.stopMoveUp(),
        'ArrowDown': () => playerRight.stopMoveDown(),
    }

    init();
    onePlayer.addEventListener('click', () => {
        startGame(false, true);
    });
    twoPlayer.addEventListener('click', () => {
        startGame(false, false);
    });
    watchGame.addEventListener('click', () => {
        startGame(true, true);
    });

    function startGame(isLeftBot, isRightBot) {
        if (isLeftBot) {
            AI[0] = updateAI;
        }
        if (isRightBot) {
            AI[1] = updateAI;
        }
        createBall(BALL_RADIUS);
        spawnDebugThings();
        connectPlayer(playerRight, new THREE.Vector3(0, 0.5, 10), './models/glb/WoodLog.glb');
        connectPlayer(playerLeft, new THREE.Vector3(0, 0.5, -10), './models/glb/WoodLogMoss.glb');
        animate();
        hideMenu();
    }

    function init() {
        setGameCamera();
        scene = new THREE.Scene();
        scene.background = new THREE.Color(0x31E738);
        loadScene();
        hideScore();
        // renderer
        renderer = new THREE.WebGLRenderer();
        renderer.setPixelRatio(window.devicePixelRatio);
        renderer.setSize(window.innerWidth, window.innerHeight);
        document.body.appendChild(renderer.domElement);
        window.addEventListener('resize', onWindowResize, false);
        setupKeyLogger();
        initSounds();
    }

    function initSounds() {
        //load sounds
        // create an AudioListener and add it to the camera
        const listener = new THREE.AudioListener();
        camera.add(listener);

        // create a global audio source
        const sound = new THREE.Audio(listener);

        // load a sound and set it as the Audio object's buffer
        const audioLoader = new THREE.AudioLoader();
        audioLoader.load('sound/music.mp3', function (buffer) {
            sound.setBuffer(buffer);
            sound.setLoop(true);
            sound.setVolume(MUSIC_VOLUME);
            sound.play();
        });
        bounceSound = new THREE.Audio(listener);
        audioLoader.load('sound/ballBounceWall.wav', function (buffer) {
            bounceSound.setBuffer(buffer);
            bounceSound.setLoop(false);
            bounceSound.setVolume(SFX_VOLUME);
        });
    }

    function setGameCamera() {
        camera = new THREE.PerspectiveCamera(38, window.innerWidth / window.innerHeight, 0.1, 1000);
        camera.position.x = -22;
        camera.position.y = 22;
        camera.position.z = -1;
        camera.rotation.x = -1.6;
        camera.rotation.y = -0.72;
        camera.rotation.z = -1.6;
    }

// LOAD ASSETS
    function loadScene() {
        // Instantiate a loader
        const loader = new THREE.GLTFLoader();
        // Load a glTF resource
        loader.load(
            // resource URL
            'models/glb/GameLevel.glb',
            // called when the resource is loaded
            function (gltf) {
                level = gltf.scene;
                level.position.x += 2;
                //fix Ground import material
                level.children[0].material = new THREE.MeshBasicMaterial({
                    color: 0x31E738
                });
                scene.add(level);
                render();
            },
            // called while loading is progressing
            function (xhr) {
                console.log((xhr.loaded / xhr.total * 100) + '% loaded');
            },
            // called when loading has errors
            function (error) {
                console.log('An error happened');
            }
        );
    }

    function loadGLTF(path, onLoad) {
        const loader = new THREE.GLTFLoader();
        loader.load(
            // resource URL
            path,
            // called when the resource is loaded
            function (gltf) {
                onLoad(gltf)
                /*
                    level = gltf.scene;
                    level.position.x += 2;
                    //fix Ground import material
                    level.children[0].material = new THREE.MeshBasicMaterial({
                        color: 0x31E738
                    });
                    scene.add(level);
                    render();
                 */
            },
            // called while loading is progressing
            function (xhr) {
                console.log((xhr.loaded / xhr.total * 100) + '% loaded');
            },
            // called when loading has errors
            function (error) {
                console.log('An error happened');
            }
        );
    }

    function setupKeyLogger() {
        document.onkeydown = function (e) {
            if (camera === undefined) {
                return;
            }
            if (e.key in keyMapDown) {
                keyMapDown[e.key]();
            }
        }
        document.onkeyup = function (e) {
            if (camera === undefined) {
                return;
            }
            if (e.key in keyMapUp) {
                keyMapUp[e.key]();
            }
        }
    }

// GAME LOOP
    function animate() {
        requestAnimationFrame(animate);
        if (playerRight.obj != null && playerLeft.obj != null && ball !=null) {
            let deltaTime = clock.getDelta();
            updatePlayer(playerLeft, deltaTime, AI[0]);
            updatePlayer(playerRight, deltaTime, AI[1]);
            updateBall(deltaTime);
            displayScore();
        }
        // Render scene
        render();
    }

    function updateBall(deltaTime) {
        moveBallInDirection(deltaTime)
        let leave = checkAndLockInArena(ball.obj, ball.size)
        if (leave.x) {
            bounce(ball, true, false);
        }
        if (leave.z) {
            bounce(ball, false, true);
        }
        if (checkBallCollision(ball)) {
            moveBallInDirection(deltaTime)
        }
    }

    function updatePlayer(player, deltaTime, ai = null) {
        if (ai) {
            ai(ball, player);
        }
        player.update(deltaTime);
        let leave = checkAndLockInArena(player.obj, player.size)
    }

    function render() {
        renderer.render(scene, camera);
        // Update draw statistics
        //Debug stuff
        if (!RELEASE) {
            stats.update();
        }
    }

// BALL
    function createBall(radius, direction = null) {
        const geometry = new THREE.SphereGeometry(radius, 32, 16);
        const material = new THREE.MeshBasicMaterial({color: 0xffff00});
        const sphere = new THREE.Mesh(geometry, material);
        direction = direction ?? new THREE.Vector3(1, 0, 1);
        sphere.position.y = radius / 2;
        loadGLTF("./models/glb/pumpkin.glb", (gltf)=>{
            ball = {
                obj: gltf.scene,
                size: (new THREE.Vector3(1, 1, 1)).multiplyScalar(radius),
                speed: BALL_INITIAL_SPEED,
                direction: direction,
                material: material,
            };
            scene.add(ball.obj);
        });
    }

    function checkBallCollision(b) {
        return checkBallCollisionWith(playerLeft, b) || checkBallCollisionWith(playerRight, b);
    }

    function checkBallCollisionWith(player, b) {
        const pPos = player.obj.position;
        const bPos = b.obj.position;
        const r = b.size.x;
        const size = player.size;

        if (
            ((Math.abs(pPos.x - bPos.x) < (size.x / 2 + r)) && (Math.abs(pPos.z - bPos.z) < (size.z / 2 + r)))
            ||
            (bPos.distanceTo(pPos) < r)
        ) {
            if (Math.abs(pPos.x - bPos.x) < (size.x / 2 + r)) {
                b.obj.position.x += -Math.sign(pPos.x - bPos.x) * b.size.x;
            }
            if (Math.abs(pPos.z - bPos.z) < (size.z / 2 + r)) {
                b.obj.position.z += -Math.sign(pPos.z - bPos.z) * b.size.z;
            }
            bounce(b, true, true);
            return true;
        }
        return false;
    }

    function bounce(b, bounceX, bounceZ) {
        if (bounceSound.isPlaying) {
            bounceSound.stop();
        }
        bounceSound.play();
        if (bounceX) {
            let x = -b.direction.x;
            x += (Math.random() > 0.5 ? 0.2 : -0.2) * x;
            b.direction.x = x;
        }
        if (bounceZ) {
            b.direction.z = -b.direction.z * (Math.random() < 0.5 ? 0.2 : 1.2);
        }
        b.direction.normalize();
        if (Math.abs(b.direction.z) < 0.3) {
            b.direction.z = 0.3 * Math.sign(b.direction.z);
            b.direction.normalize();
        }
        b.speed = Math.min(b.speed + BALL_INITIAL_SPEED * 0.1, BALL_MAX_SPEED);
    }

    function resetBall() {
        ball.obj.position.set(0, ball.obj.position.y, 0);
        ball.speed = BALL_INITIAL_SPEED;
        let z = random(-1, 1);
        if (Math.abs(z) < 0.4) {
            z = 0.1 * Math.sign(z);
        }
        ball.direction.set(random(-0.75, 0.75), 0, z).normalize();
    }

    function moveBallInDirection(deltaTime) {
        ball.obj.position.z += ball.direction.z * ball.speed * deltaTime;
        ball.obj.position.x += ball.direction.x * ball.speed * deltaTime;
    }

// PLAYER
    function connectPlayer(player, pos, modelPath) {
        loadGLTF(modelPath, (gltf) => {
            let obj = gltf.scene;
            scene.add(obj);
            obj.rotation.y = Math.PI / 2
            obj.scale.multiplyScalar(1);

            const geometry = new THREE.BoxGeometry(PLAYER_SIZE.x, PLAYER_SIZE.y, PLAYER_SIZE.z);
            const material = new THREE.MeshBasicMaterial({color: 0xdd1100});
            const box = new THREE.Mesh(geometry, material);
            box.rotation.y = Math.PI / 2
            obj.add(box);
            player.connectObject(obj);
            player.setPosition(pos.x, pos.y, pos.z);
        });
    }


    function checkAndLockInArena(obj, size) {
        let leaveBoundary = {
            x: false,
            y: false,
            z: false,
        };
        if (Math.abs(obj.position.x) > arenaSize.x / 2 - size.x / 2) {
            obj.position.x = (arenaSize.x / 2 - size.x / 2) * Math.sign(obj.position.x)
            leaveBoundary.x = true;
        }
        if (Math.abs(obj.position.z) > arenaSize.z / 2 - size.z / 2) {
            obj.position.z = (arenaSize.z / 2 - size.z / 2) * Math.sign(obj.position.z)
            leaveBoundary.z = true;
        }
        return leaveBoundary;
    }

// AI
    function updateAI(b, p) {
        p.direction = 0;
        if (Math.sign(b.direction.z) !== Math.sign(p.obj.position.z - b.obj.position.z)) {
            return;
        }
        if (Math.abs(b.obj.position.x - p.obj.position.x) > (p.size.x / 2) * random(0.1, 0.9)) {
            p.direction = Math.sign(b.obj.position.x - p.obj.position.x) * random(0, 0.8);
        }
    }

// SCORE
    function checkScore() {
        if (ball.obj.position.z > 15) {
            return -1
        }
        if (ball.obj.position.z < -15) {
            return 1
        }
        return 0
    }

    function displayScore() {
        const scoreCurrent = checkScore();
        if (scoreCurrent === 0) {
            return;
        }
        resetBall();
        if (scoreCurrent === -1) {
            playerLeft.score += 1;
        }
        if (scoreCurrent === 1) {
            playerRight.score += 1;
        }
        scoreLeft.textContent = playerLeft.score;
        scoreRight.textContent = playerRight.score;
    }

    function hideScore() {
        scoreLeft.textContent = "";
        scoreRight.textContent = "";
    }

    function hideMenu() {
        menu.style.display = 'none';
        scoreLeft.textContent = playerLeft.score;
        scoreRight.textContent = playerRight.score;
    }

// TOOL
    function random(min, max) {
        return Math.random() * (max - min) + min;
    }

    function spawnDebugThings() {
        if (RELEASE) {
            return;
        }
        // Add helper object (bounding box)
        var box_geometry = new THREE.BoxGeometry(20.01, 1.01, 40.01);
        var box_mesh = new THREE.Mesh(box_geometry, null);

        var bbox = new THREE.BoundingBoxHelper(box_mesh, 0xffffff);
        scene.add(bbox);
        bbox.update();


        // Display statistics of drawing to canvas
        stats = new Stats();
        stats.domElement.style.position = 'absolute';
        stats.domElement.style.top = '0px';
        stats.domElement.style.zIndex = 100;
        document.body.appendChild(stats.domElement);
    }

    function onWindowResize() {
        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();
        renderer.setSize(window.innerWidth, window.innerHeight);
        render();
    }
}
