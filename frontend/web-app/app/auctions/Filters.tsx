import { useParamsStore } from '@/hooks/useParamsStore';
import { Button, ButtonGroup } from 'flowbite-react';
import { stat } from 'fs';
import React from 'react'
import { AiOutlineClockCircle, AiOutlineSortAscending } from 'react-icons/ai';
import { BsFillStopCircleFill, BsStopBtnFill, BsStopwatchFill } from 'react-icons/bs';
import { GiFinishLine, GiFlame } from 'react-icons/gi';


const pageCountNumbers = [4, 8, 12];
const orderByFilters = [
    { label: 'Alphabetical', icon: AiOutlineSortAscending, value: 'make' },
    { label: 'End Date', icon: AiOutlineClockCircle, value: 'endginsooon' },
    { label: 'Recently Added', icon: BsFillStopCircleFill, value: 'new' },
]

const filterByData = [
    { label: 'Live Actions', icon: GiFlame, value: 'live' },
    { label: 'Ending < 6hrs', icon: GiFinishLine, value: 'EndingSoon' },
    { label: 'Finished', icon: BsStopwatchFill, value: 'Finished' },
]



export default function Filters() {
    const pageSize = useParamsStore(state => state.pageSize);
    const setPageCount = useParamsStore(state => state.setParams);
    const setParams = useParamsStore(state => state.setParams);
    const orderBy = useParamsStore(state => state.orderBy);
    const filterBy = useParamsStore(state => state.filterBy);

    return (
        <div className='flex justify-between items-center mb-4'>
            <div>
                <span className='uppercase text-sm text-gray-400 mr-2'>FILTER BY</span>
                <ButtonGroup>
                    {
                        filterByData.map(({ label, icon: Icon, value }) => (
                            <Button
                                key={value}
                                onClick={() => setParams({ filterBy: value })}
                                color={`${filterBy === value ? "red" : "gray"}`}
                                className='focus:ring-0'
                            >
                                <Icon className='mr-3 h-3 w-3' />
                                {label}
                            </Button>
                        ))
                    }
                </ButtonGroup>
            </div>
            <div>
                <span className='uppercase text-sm text-gray-400 mr-2'>ORDER BY</span>
                <ButtonGroup>
                    {
                        orderByFilters.map(({ label, icon: Icon, value }) => (
                            <Button
                                key={value}
                                onClick={() => setParams({ orderBy: value })}
                                color={`${orderBy === value ? "red" : "gray"}`}
                                className='focus:ring-0'
                            >
                                <Icon className='mr-3 h-3 w-3' />
                                {label}
                            </Button>
                        ))
                    }
                </ButtonGroup>
            </div>
            <div>
                <span className='uppercase text-sm text-gray-400 mr-2'>PAGE SIZE</span>
                <ButtonGroup>
                    {
                        pageCountNumbers.map((value, index) => (
                            <Button
                                key={index}
                                onClick={() => setPageCount({ pageCount: value })}
                                color={`${pageSize === value ? "red" : "gray"}`}
                                className='focus:ring-0'
                            >
                                {value}
                            </Button>
                        ))
                    }
                </ButtonGroup>
            </div>
        </div>
    )
}
