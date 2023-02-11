export enum RealizationStatus {
    Pending = 0,
    Shipment = 1,
    Delivered = 2,

    Lost = -1
}

export const RealizationStatusToString = (status: RealizationStatus) => {
    if (status == 0) {
        return 'Pending'
    }
    if (status == 1) {
        return 'Shipment'
    }
    if (status == 2) {
        return 'Delivered'
    }

    return 'Lost'
}

export const StatusColor = (status: RealizationStatus) => {
    if(status == RealizationStatus.Pending){
        return 'warning'
      }
      if(status == RealizationStatus.Shipment){
        return 'info'
      }
      if(status == RealizationStatus.Delivered) {
        return 'success'
      }
  
      return 'danger'
}