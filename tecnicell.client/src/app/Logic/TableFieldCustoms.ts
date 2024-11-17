import { StyleField } from "../Interfaces/tools/Table/TableField"

export const MoneyStyleCustom = [
    {
      condition : {
        formControlName: "actionHistory",
        value : 'Salida'
      },
      style:'green'
    },
    {
      condition : {
        formControlName: "actionHistory",
        value : 'Entrada'
      },
      style:'red'
    },
    {
      condition : {
        formControlName: "actionHistory",
        value : undefined
      },
      style:'green'
    },
  ]
export const ActionStyleCustom = [
    {
        condition : {
        formControlName: "actionHistory",
        value : 'Entrada'
        },
        style:'green'
    },
    {
        condition : {
        formControlName: "actionHistory",
        value : 'Salida'
        },
        style:'red'
    },
    {
        condition : {
        formControlName: "actionHistory",
        value : 'Transferido hacia otra Sucursal'
        },
        style:'green'
    },
    {
        condition : {
        formControlName: "actionHistory",
        value : 'Merma'
        },
        style:'gray'
    },
    {
        condition : {
        formControlName: "actionHistory",
        value : 'Pieza Extraida'
        },
        style:'yellow'
    },
    {
        condition : {
        formControlName: "actionHistory",
        value : 'Pieza Puesta'
        },
        style:'yellow'
    },
    {
        condition : {
        formControlName: "actionHistory",
        value : 'Armado'
        },
        style:'orange'
    },
    {
        condition : {
        formControlName: "actionHistory",
        value : 'Reparado'
        },
        style:'orange'
    },
]
export const StateStyleCustom = [
    {
        condition : {
        formControlName: "available",
        value : 'Disponible'
        },
        style:'green'
    },
    {
        condition : {
        formControlName: "available",
        value : 'No Disponible'
        },
        style:'red'
    },
]

export const CurrentStateStyleCustom = [
    {
        condition : {
        formControlName: "currentState",
        value : 'Entregado'
        },
        style:'green'
    },
    {
        condition : {
        formControlName: "currentState",
        value : 'Listo'
        },
        style:'orange'
    },
    {
        condition : {
        formControlName: "currentState",
        value : 'Reparando'
        },
        style:'red'
    },
]