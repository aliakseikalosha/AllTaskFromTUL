//
// Created by Aliaksei Kalosha on 21.03.2022.
//

#pragma once

#include "glm/gtc/type_ptr.hpp"
#include "./Avatars/Camera.h"
#include "./Avatars/Player.h"
#include "../GL/input.h"
#include "Avatars/SceneObject.h"
#include "../DataLoader/texture.h"
#include "Avatars/Light.h"
#include "Avatars/Actor.h"
#include "Data/SpriteData.h"
#include "Renderer/ActorRenderer.h"
#include "Config.h"

#define ONE_TILE_SIZE (glm::vec2(TILE, TILE))

class Game {
protected:
    glm::vec3 offset = glm::vec3(0, 0, 0);

    void DrawTransparent(const glm::mat4 &projectionMatrix, const float &dt);

public:
    Game() { Init(); }

    ~Game() {
        delete player;
        delete camera;
    }

    Player *player;
    Camera *camera;
    std::vector<SceneObject> objects;
    std::vector<Flask> actors;
    std::vector<Light> lights;
    glm::mat4 viewMat;

    SpriteData flaskSprites[4] = {SpriteData(glm::vec2(18 * TILE + TILE * 1, 15 * TILE), ONE_TILE_SIZE),
                                  SpriteData(glm::vec2(18 * TILE + TILE * 2, 15 * TILE), ONE_TILE_SIZE),
                                  SpriteData(glm::vec2(18 * TILE + TILE * 3, 15 * TILE), ONE_TILE_SIZE),
                                  SpriteData(glm::vec2(18 * TILE + TILE * 4, 15 * TILE), ONE_TILE_SIZE),};

    SpriteData wallSprites[9] = {SpriteData(glm::vec2(2 * TILE, 2 * TILE), ONE_TILE_SIZE),
                                 SpriteData(glm::vec2(3 * TILE, 2 * TILE), ONE_TILE_SIZE),
                                 SpriteData(glm::vec2(4 * TILE, 2 * TILE), ONE_TILE_SIZE),

                                 SpriteData(glm::vec2(2 * TILE, 3 * TILE), ONE_TILE_SIZE),
                                 SpriteData(glm::vec2(3 * TILE, 3 * TILE), ONE_TILE_SIZE),
                                 SpriteData(glm::vec2(4 * TILE, 3 * TILE), ONE_TILE_SIZE),

                                 SpriteData(glm::vec2(2 * TILE, 4 * TILE), ONE_TILE_SIZE),
                                 SpriteData(glm::vec2(3 * TILE, 4 * TILE), ONE_TILE_SIZE),
                                 SpriteData(glm::vec2(4 * TILE, 4 * TILE), ONE_TILE_SIZE),};

    SpriteData floorSprites[8] = {SpriteData(glm::vec2(2 * TILE, 5 * TILE), ONE_TILE_SIZE),
                                  SpriteData(glm::vec2(3 * TILE, 5 * TILE), ONE_TILE_SIZE),
                                  SpriteData(glm::vec2(4 * TILE, 5 * TILE), ONE_TILE_SIZE),

                                  SpriteData(glm::vec2(2 * TILE, 6 * TILE), ONE_TILE_SIZE),
                                  SpriteData(glm::vec2(3 * TILE, 6 * TILE), ONE_TILE_SIZE),
                                  SpriteData(glm::vec2(4 * TILE, 6 * TILE), ONE_TILE_SIZE),

                                  SpriteData(glm::vec2(2 * TILE, 7 * TILE), ONE_TILE_SIZE),
                                  SpriteData(glm::vec2(3 * TILE, 7 * TILE), ONE_TILE_SIZE),};

    void Init() {
        player = new Player(glm::vec3(0.0, 0.0, 0.0), glm::vec2(0, 0), 5);
        camera = new Camera(glm::vec3(0, 1, -5), glm::vec2(0, 0));
        loadLevel("../resources/map/lvl01.txt");
    }

    void placeLight(const glm::vec3 &pos, const glm::vec4 color) {
        Light *l = new Light(pos, glm::vec2(0, 0), color);
        lights.push_back(*l);
    }

    void placeFlask(const glm::vec3 pos, int id, GLuint textureId, GLuint normalId) {
        Flask a = Flask(pos, glm::vec2(0, 0));
        a.renderer = new ActorRenderer();
        Mesh m;
        customQuadV(m.vertex, m.index, 0.5, 0.5, flaskSprites[id].bottomLeft, flaskSprites[id].size.x,
                    flaskSprites[id].size.y);
        a.renderer->init(m, "../resources/shader/textureBilboard.vert", "../resources/shader/textureBilboard.frag",
                         textureId, normalId);
        actors.push_back(a);
    }

    void placeWall(const glm::vec3 &pos, int id, GLuint textureId, GLuint normalId) {
        Mesh m;
        m.loadOBJ("../resources/model/cube_triangles_normals_tex.obj", wallSprites[id].bottomLeft,
                  wallSprites[id].size.x, WORLD_CELL_SIZE);

        SceneObject *sceneObject = new SceneObject(pos + glm::vec3(0, 0, 1.0) * WORLD_CELL_SIZE, glm::vec2(0, 0));
        sceneObject->renderer = new MeshRenderer();
        sceneObject->renderer->init(m, "../resources/shader/texture.vert", "../resources/shader/texture.frag",
                                    textureId, normalId);
        objects.push_back(*sceneObject);
    }

    void placeFloor(const glm::vec3 &pos, int id, GLuint textureId, GLuint normalId) {
        Mesh m;
        SceneObject *sceneObject = new SceneObject(pos, glm::vec2(0, 0));
        sceneObject->renderer = new MeshRenderer();
        //m.loadOBJ("../resources/model/cube_triangles_normals_tex.obj", floorSprites[id].bottomLeft, floorSprites[id].size.x,  WORLD_CELL_SIZE);

        m.loadFloor(floorSprites[id].bottomLeft, floorSprites[id].size.x, WORLD_CELL_SIZE);
        sceneObject->renderer->init(m, "../resources/shader/texture.vert", "../resources/shader/texture.frag",
                                    textureId, normalId);
        objects.push_back(*sceneObject);
    }

    void loadLevel(const char *levelPath) {
        GLuint textureId = textureInit("../resources/texture/0x72_DungeonTilesetII_v1.4.png", false);
        GLuint normalId = textureInit("../resources/texture/normalTest.png", false);
        glm::vec3 pos;

/*
        std::ifstream t(levelPath);
        std::string str((std::istreambuf_iterator<char>(t)), std::istreambuf_iterator<char>());
        std::stringstream ss(str.c_str());
        std::string to;

        for( int z = 0;std::getline(ss,to,'\n');z++){
            for (int x = 0; x < to.length(); ++x) {
                pos = glm::vec3(x * WORLD_CELL_SIZE, -1, z * WORLD_CELL_SIZE);
                if (to[x] == '#' ) {
                    placeWall(pos, (x + z) % 9, textureId, normalId);
                    placeWall(pos + glm::vec3(0, 1, 0) * WORLD_CELL_SIZE, (x + z) % 9, textureId, normalId);
                } else {
                    placeFloor(pos, (x / 2 + z / 4) % 8, textureId, normalId);
                    if(to[x] == 'f'){

                        placeLight( pos + glm::vec3(0.45, 1.5, 0.45), glm::vec4(1.0, 1.0, 1.0, 1));
                        placeFlask( pos + glm::vec3(0.5, 1, 0.5), 3, textureId, normalId);
                    }
                    if(to[x] == 'S'){
                        player = new Player(pos + glm::vec3(0.5, 1, 0.5), glm::vec2(0, 0), 5);
                    }
                }
            }
        }
*/
        float worldSize = 10;
        for (int x = 0; x <= worldSize; ++x) {
            for (int z = 0; z <= worldSize; ++z) {
                pos = glm::vec3((x - worldSize / 2.0) * WORLD_CELL_SIZE, -1, (z - worldSize / 2.0) * WORLD_CELL_SIZE);
                if (x == 0 || x == worldSize || z == worldSize || z == 0) {
                    placeWall(pos, (x + z) % 9, textureId, normalId);
                    placeWall(pos + glm::vec3(0, 1, 0) * WORLD_CELL_SIZE, (x + z) % 9, textureId, normalId);
                } else {
                    placeFloor(pos, (x / 2 + z / 4) % 8, textureId, normalId);
                }
            }
        }
        //light
        placeLight(glm::vec3(0.0, 1, 0.0), glm::vec4(1.0, 1.0, 1.0, 1));
        placeLight(glm::vec3(2.0, 1, 2.0), glm::vec4(1.0, 0.0, 0.0, 1));
        placeLight(glm::vec3(2.0, 1, 0.0), glm::vec4(0.0, 1.0, 0.0, 1));
        placeLight(glm::vec3(0.0, 1, 2.0), glm::vec4(0.0, 0.0, 1.0, 1));
        //objects
        placeFlask(glm::vec3(3.0, 0.2, 0.0), 3, textureId, normalId);
        placeFlask(glm::vec3(0.0, 0.2, 0.0), 0, textureId, normalId);
        placeFlask(glm::vec3(1.0, 0.2, 0.0), 1, textureId, normalId);
        placeFlask(glm::vec3(2.0, 0.2, 0.0), 2, textureId, normalId);

    }

    void Update(const float &dt) {
        player->ProcessInput(inputState.moveDelta, inputState.lookDelta, dt);
        camera->UpdateFollow(*player, offset);
        viewMat = glm::lookAt(camera->getPos(), camera->forward() + camera->getPos(), glm::vec3(0.0f, 1.0f, 0.0f));
        for (auto &actor: actors) {
            actor.update(dt);
        }
    }

    void Draw(const glm::mat4 &projectionMatrix, const float &dt) {

        for (auto &obj: objects) {
            if (obj.renderer != NULL) {
                obj.renderer->render(projectionMatrix, viewMat, dt, obj.getPos(), getLightsCloseTo(obj.getPos()));
            }
        }

        for (auto &obj: lights) {
            if (obj.renderer != NULL) {
                ((LightRenderer *) obj.renderer)->render(projectionMatrix, viewMat, dt, obj.getPos(),
                                                         getLightsCloseTo(obj.getPos()));
            }
        }

        DrawTransparent(projectionMatrix, dt);

        resetLook();
    }

    std::vector<LightData> getLightsCloseTo(const glm::vec3 pos) {
        std::vector<LightData> l;

        for (auto &light: lights) {
            if (true){//glm::length(pos - light.getPos()) < 2.5 * WORLD_CELL_SIZE) {
                l.push_back(light.getData());
            }
        }

        std::sort(l.begin(), l.end(), [pos](LightData &a, LightData &b) -> bool {
            return glm::length(pos - a.pos) < glm::length(pos - b.pos);
        });

        return l;
    }
};

void Game::DrawTransparent(const glm::mat4 &projectionMatrix, const float &dt) {
    glEnable(GL_BLEND);
    glDepthMask(GL_FALSE);
    glm::vec3 playerPos = player->getPos();
    sort(actors.begin(), actors.end(), [playerPos](Actor &a, Actor &b) -> bool {
        return glm::length(playerPos - a.getPos()) > glm::length(playerPos - b.getPos());
    });


    for (auto &obj: actors) {
        if (obj.renderer != NULL) {
            obj.renderer->render(projectionMatrix, viewMat, dt, obj.getPos(), getLightsCloseTo(obj.getPos()));
        }
    }
    glDisable(GL_BLEND);
    glDepthMask(GL_TRUE);
}


