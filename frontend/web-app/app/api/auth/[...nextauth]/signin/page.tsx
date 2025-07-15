import EmptyFilter from '@/components/EmptyFilter'
import React from 'react'

export default function SignIn({ searchParams }: { searchParams: { callbackUrl: string } }) {
  return (
    <>
      <EmptyFilter
        title='you be logged in to access this Url'
        showLogin
        callbackUrl={searchParams.callbackUrl}
      />

    </>
  )
}
