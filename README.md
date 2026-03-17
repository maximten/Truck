## State
* Money
* TruckState
    * Wait
    * Departing
    * Riding
## Entities
* Settings
    * BoxPrice
    * TruckDelay
    * DepartureTime
* Player
    * MoveBehavior
    * BoxHandlingBehavior
    * SendTruckBehavior
    * KnockOutBehavior
* Box
* BoxSpawner
* Truck
* ParkingPoint
* LoadTrigger

* LoadingBehavior
    * InLoadingZone
* SendTruckBehavior
    * InLoadingZone
* LoadingZoneBehavior
    * ParkingZone
* ParkingZoneBehavior
    * HasTruck
* Truck
    * TruckState State
    * List<Box> Storage
    * HandleState
