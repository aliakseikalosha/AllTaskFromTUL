//
// Created by Aliaksei Kalosha on 21.03.2022.
//

#include "glm/gtc/type_ptr.hpp"
#include "Avatars/Camera.h"
#include "Avatars/Player.h"
#include "input.h"

class Game {
protected:
    glm::vec3 offset = glm::vec3(0, 0, 0);
public:
    Game() { Init(); }
    ~Game(){
        delete player;
        delete camera;
    }
    Player *player;
    Camera *camera;
    glm::mat4 viewMat;

    void Init() {
        player = new Player(glm::vec3(0, 0, 0), glm::vec2(0, 0), 5);
        camera = new Camera(glm::vec3(0, 0, -5), glm::vec2(0, 0));
    }

    void Update(const float &dt) {
        player->ProcessInput(inputState.moveDelta, inputState.lookDelta, dt);
        camera->UpdateFollow(*player, offset);
        viewMat = glm::lookAt(camera->getPos(), camera->forvard() + camera->getPos(), glm::vec3(0.0f, 1.0f, 0.0f));
        camera->printState();
        //player->printState();
    }
};
