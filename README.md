# ProjectMercator — симулятор вездехода на Unity (HDRP)

3D-прототип про управление вездеходом: давление в шинах, температура двигателя с зонами
перегрева, рычаг, зоны фокуса камеры, инвентарь и подбор предметов. Характеристики машины и
игрока вынесены в `ScriptableObject`-конфиги, поэтому баланс меняется без правки кода.

Проект собран в паре с 3D-артистом: я отвечал за код и интеграцию, визуал — Shader Graph и
VFX Graph (снегопад с управляемыми извне скоростью падения и углом наклона).

## Что реализовано

**Механики вездехода**
- `Vehicle_PressureBuildup` / `Vehicle_PressureIndicator` — набор и сброс давления, индикация.
- `Vehicle_TemperatureBuildup` / `Vehicle_TempSwitcher` / `Vehicle_TemperatureIndicator` —
  нагрев, зоны перегрева, переключение режимов.
- `Vehicle_Lever` — рычаг как отдельный интерактивный объект с состоянием.
- `Vehicle_FocusZone` — зоны фокуса камеры.
- `Vehicle_InOut` / `Vehicle_StartMoving` — посадка/высадка и старт движения.

**Игрок и взаимодействия**
- `Player_Move`, `Player_CameraMove`, `Player_Input` (Input System, сгенерированные экшены).
- `Player_Interact` + интерфейсы `IInteractable` / `IMouseInteractable` — единая точка входа
  для всех интерактивных объектов, без прямых ссылок между системами.
- `Player_Inventory`, `Item_PickUp`, `Item_CrystalPickUp` — подбор и хранение предметов.

**Архитектура**
- `PlayerSettings` / `VehicleSettings` (`ScriptableObject`) — конфигурация характеристик.
- `EventBus` + `Event` — события между системами без жёстких связей.
- `Global_TimeTicker` — единый тик для временных механик.
- `Generic_Trigger` + `TriggerType_Enum` — переиспользуемый триггер с типом срабатывания.

## Структура

```
Assets/ProjectMercator/
├── Scenes/DevelopScene.unity   основная сцена
└── Scripts/
    ├── Vehicle/                механики вездехода (давление, температура, рычаг, фокус)
    ├── Player/                 движение, камера, ввод, инвентарь, взаимодействие
    ├── Items/                  предметы и подбор
    ├── ScriptableObjects/      конфиги игрока и вездехода
    ├── System/                 EventBus, таймер, интерфейсы, generic-триггер
    └── Generated_Scripts/      сгенерированный Input System (не ручной код)
```

## Стек

Unity 6 (6000.0.68f1), HDRP 17.0.4, C#, Input System, Shader Graph, VFX Graph,
ScriptableObject-конфигурация, Git.

## Известные ограничения

- Одна сцена (`DevelopScene`), без билда в репозитории: проект запускается из редактора Unity 6.
- Нет сохранений, мета-прогрессии и сетевого режима — это вертикальный срез механик.
- `Generated_Scripts/InputSystem_Action.cs` (653 строки) сгенерирован Input System, а не написан
  руками; свой код — остальные ~940 строк.
