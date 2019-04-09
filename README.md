# ZombieTrap

[Видео демки](https://www.youtube.com/watch?v=Av0AVdlHNXU)

В замкнутых комнатах бегают два вида зомби, по ним периодически бьёт фаерболом. В каждой комнате могут находиться до двух игроков. При подключении большего количества игроков создаётся новая комната. 

Взаимодействие между сервером и клиентом осуществляется с использованием UdpClient. При этом, существует две очереди сообщений:
- Строгая очередь сообщений дожидается, пока клиент перешлёт подтверждение получения сообщения (к примеру, при коннекте к серверу или получении списка объектов в комнате). 
- Обычная очередь сообщений не дожидается ответа от клиента, к примеру при передаче текущих позиций зомби.

[Исходный код сервера](https://github.com/fornetjob/ZombieTrap/tree/master/ZombieTrap/Server/ServerApplication/ServerApplication/Features)

[Исходный код клиента](https://github.com/fornetjob/ZombieTrap/tree/master/ZombieTrap/Assets/Scripts/Features)

## Описание

1. На сервере создаются комнаты, в которой находятся предметы обстановки и передвигающиеся зомби. За передвижение зомби отвечает система: [ItemsMoveSystem](https://github.com/fornetjob/ZombieTrap/blob/master/ZombieTrap/Server/ServerApplication/ServerApplication/Features/Items/ItemsMoveSystem.cs).
2. Клиент при подключении посылает сообщение со своим идентификатором и получает в ответ номер комнаты, в которой он находится, серверное время для синхронизации и находящиеся в комнате объекты.
4. Раз в [FixedDeltaTime](https://github.com/fornetjob/ZombieTrap/blob/master/ZombieTrap/Server/ServerApplication/ServerApplication/App.config) сервер посылает всем игрокам координаты зомби в их комнате.
5. Если зомби в комнате меньше, чем [MaxZombieCount](https://github.com/fornetjob/ZombieTrap/blob/master/ZombieTrap/Server/ServerApplication/ServerApplication/Features/Rooms/Room.cs), добавляется новый зомби в системе [ZombieSpawnSystem](https://github.com/fornetjob/ZombieTrap/blob/master/ZombieTrap/Server/ServerApplication/ServerApplication/Features/Zombies/ZombiesSpawnSystem.cs).
6. Периодически в комнате возникает фаербол, который бьёт по зомби (система [ProjectileSpawnerSystem](https://github.com/fornetjob/ZombieTrap/blob/master/ZombieTrap/Server/ServerApplication/ServerApplication/Features/Projectiles/ProjectileSpawnerSystem.cs)).
7. Если фаербол наносит урон, игроку пересылается новое количество хелсов повреждённых объектов (система [ProjectileExplosionSystem](https://github.com/fornetjob/ZombieTrap/blob/master/ZombieTrap/Server/ServerApplication/ServerApplication/Features/Projectiles/ProjectileExplosionSystem.cs)).
8. На клиенте при получении информации о повреждении проигрывается анимация урона по зомби. Если хелсы опустились до нуля - проигрывается анимация уничтожения зомби.
