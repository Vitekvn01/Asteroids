public enum PhysicsLayer
{
    None = 0,                // 0000
    Player = 1 << 0,         // 0001
    Enemy = 1 << 1,          // 0010
    Asteroid = 1 << 2,       // 0100
    Bullet = 1 << 3,         // 1000
    Default = 1 << 4,        // 1 0000 — новый универсальный
    All = ~0               // Всё, кроме None
}