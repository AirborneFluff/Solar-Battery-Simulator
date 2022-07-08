export interface VirtualBatterySystem {
  id: number,
  loggingPeriod: number,
  chargeLevel: number,
  chargePercentage: string,
  totalCapacity: number,
  usableCapacity: number,
  depthOfDischarge: number,
  continuousDischargeRate: number,
  continuousChargeRate: number,
  chargeEfficiency: number,
  dischargeEfficiency: number,
}
